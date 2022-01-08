import { ILocations } from "../../interfaces/locations";
import { api, source } from "./api";

export async function postUserLocation(
    token:string, latitude:number, longitude:number, timestamp:string) {

    const response = await api.post<ILocations>(`/User/${token}/locations`, {
      token: token,
      lat: latitude.toFixed(5),
      long: longitude.toFixed(5),
      datetime: timestamp
    }, 
    {
      cancelToken: source.token,
      headers: {token: token}
    })
    .then(response => response.data)
    .catch(error => console.log(error))
    return response
  }


/*   export async function getLocation( token: string, latitude:number, longitude:number) {
    const response = await api.post('/posicao', {
      lat: latitude,
      lon: longitude
    }, {headers: {token: token}} )
    .then(response => response.data)
    .catch(error => console.log(error))
  
    console.log( `[api] get location lat ${latitude} e long ${longitude}` );
     
  } */