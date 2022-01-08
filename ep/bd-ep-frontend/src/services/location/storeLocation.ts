import AsyncStorage from "@react-native-async-storage/async-storage"
import { LocationObject } from "expo-location";

export const locationsStorageName = 'bg-locations'
//export const locations:LocationObject[] = [];

export async function getLocations() {
    const data = await AsyncStorage.getItem(locationsStorageName);
    return data ? JSON.parse(data) as LocationObject[] : [];
}

export async function setLocations(locations: LocationObject[]): Promise<void> {
    await AsyncStorage.setItem(locationsStorageName, JSON.stringify(locations));
}

export async function addLocation(location: LocationObject): Promise<LocationObject[]> {
    const existing = await getLocations();
    const locations = [ location, ...existing];
    //max 10 locations
    if(locations.length > 10) locations.pop();
    
    await setLocations(locations);
    console.log('[storage]', 'added location lat', location.coords.latitude, 'lon', location.coords.longitude);
    return locations;
}

export async function clearLocations(): Promise<void> {
    await AsyncStorage.removeItem(locationsStorageName);
    console.log('[storage]', 'cleared locations');
}