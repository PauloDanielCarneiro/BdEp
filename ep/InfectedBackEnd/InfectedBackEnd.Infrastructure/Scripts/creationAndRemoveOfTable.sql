CREATE EXTENSION if not exists postgis;

create table diseases
(
    id uuid not null primary key,
    "name" varchar not null unique,
    contagious boolean not null
);

create table users
(
    id uuid not null primary key,
    document char(11) not null unique,
    "name" varchar not null,
    email varchar unique,
    token uuid not null unique,
    password varchar not null
);

create table locations
(
    id uuid not null primary key,
    latitude  double precision not null,
    longitude double precision not null,
    "name" varchar not null
);

create index user_document_idx
    on users(document);

create index user_email_idx
    on users(email);

create index user_token_idx
    on users(token);

create index location_longitude_idx
    on locations(longitude);

create index location_latitude_idx
    on locations(latitude);

create index disease_name_idx
    on diseases("name");

create table user_location
(
    id uuid not null primary key,
    DateAndTime timestamp not null,
    user_id uuid not null
        constraint fk_user
            references users
            on delete restrict on update restrict,
    location_id uuid not null
        constraint fk_location
            references locations
            on delete restrict on update restrict
);

create index userlocation_dateandtime_idx
    on user_location(dateandtime);

create table user_diseases
(
    id uuid not null primary key,
    cured boolean not null,
    show_symptoms boolean not null,
    startDate timestamp not null,
    endDate timestamp not null,
    user_id uuid not null
        constraint fk_user
            references users
            on delete restrict on update restrict,
    disease_id uuid not null
        constraint fk_location
            references diseases
            on delete restrict on update restrict
);

alter table locations add column geom geometry(Point, 4326);

create or replace function public.get_relevant_data(user_id_parameter uuid, date_parameter date)
    returns TABLE(user_ids uuid, date_return timestamp, location_id uuid, cured bool, latitude double precision, longitude double precision)
    language plpgsql
as
$$
declare
    location record;
begin
    drop table if exists today_locations;
    create table today_locations(id uuid);
    insert into today_locations select ul.location_id from user_location ul where ul.user_id = user_id_parameter and ul.dateandtime >= date_parameter;

    drop table if exists temp_location;
    create table temp_location(
                                  id uuid not null primary key,
                                  latitude  double precision not null,
                                  longitude double precision not null,
                                  "name" varchar not null,
                                  geom geometry(Point, 4326)
    );
    insert into temp_location select loc.id, loc.latitude, loc.longitude, loc.name, loc.geom from locations loc where loc.id in(select id from today_locations);

    drop table if exists temp_close_locations_ids;
    create table temp_close_locations_ids(id uuid);
    for location in
        select * from temp_location
        loop
            insert into temp_close_locations_ids select id from locations loc where st_dwithin(loc.geom, st_makepoint(location.latitude, location.longitude)::geography, 10);
        end loop;


    drop table if exists temp_user_location;
    create table temp_user_location(
                                       id uuid primary key,
                                       DateAndTime timestamp,
                                       user_id uuid,
                                       location_id uuid
    );
    insert into temp_user_location
    select * from user_location ul
    where ul.location_id in (
        select * from temp_close_locations_ids)
      and ul.dateandtime >= date_parameter;

    return query
        select temp_user_location.user_id, temp_user_location.dateandtime, temp_user_location.location_id, u.cured, loc.latitude, loc.longitude
        from temp_user_location
                 inner join locations loc on loc.id = temp_user_location.location_id
                 left join user_diseases u on temp_user_location.user_id = u.user_id
        where temp_user_location.user_id
            in(
                  select ud.user_id
                  from user_diseases ud
                  where ud.cured = false)
           or temp_user_location.user_id = user_id_parameter
        order by to_char(loc.latitude, '999D9') || to_char(loc.longitude, '999D9');
end;
$$;

create index geom_location_idx on locations using gist(geom);
