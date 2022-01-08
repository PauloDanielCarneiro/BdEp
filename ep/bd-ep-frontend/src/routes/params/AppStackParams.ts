import { RouteProp } from "@react-navigation/native";
import { StackNavigationProp } from "@react-navigation/stack";
import { LocationObject } from "expo-location";

export type AppStackParamsList = {
    Details: undefined ;
    Mapas:  undefined;
    Status: undefined;
    Profile: undefined;
}
type detailsNavProp = StackNavigationProp<AppStackParamsList, 'Details'>;
type detailsRouteProp = RouteProp<AppStackParamsList, 'Details'>;
type mapasNavProp = StackNavigationProp<AppStackParamsList, 'Mapas'>;
type mapasRouteProp = RouteProp<AppStackParamsList, 'Mapas'>

export type {detailsNavProp};
export type {mapasNavProp}
export type {detailsRouteProp}
export type {mapasRouteProp}

//como passar informacao entre paginas
//usa quando for navegar para uma pagina
//ex: navigation.navigate("Details", {locations:locations});
//para usar === route.params.locations (route vem da declaracao em baixo)
export interface mapasProps{
    navigation: mapasNavProp
    route: mapasRouteProp
}

export interface detailsProps{
    navigation: detailsNavProp
    route: detailsRouteProp
}