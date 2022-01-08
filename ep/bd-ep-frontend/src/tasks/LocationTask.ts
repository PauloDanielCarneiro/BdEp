/* import { EventEmitter, LocationObject } from "expo-location";
import * as TaskManager from "expo-task-manager";
import { posicao } from "../services/api";
import { addLocation } from "../services/location/storeLocation";

 */
export const LOCATION_TASK = 'background-location-task'

/* 
TaskManager.defineTask(LOCATION_TASK, async( { data: locations , error } ) => {
  if (error) {
        console.error(error.message)
        // Error occurred - check `error.message` for more details.
        return;
  }
  if (locations) {
    //locations eh LocationObjetct[]
    //{loc: [{"locations":["coords":[object], "timestamp": integer]}]}
    const location = Object.values(locations)[0][0] as LocationObject;
    addLocation(location)
    const response = await posicao(location.coords.latitude, location.coords.longitude);

  }
})
 */
