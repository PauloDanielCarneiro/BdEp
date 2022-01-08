import * as Location from 'expo-location'
import { LOCATION_TASK } from '../../tasks/LocationTask';

export async function isTrackingLocation(): Promise<boolean>{
    const isTracking = await Location.hasStartedLocationUpdatesAsync(LOCATION_TASK)
    .then( () => true, () => false)
    .catch(error => console.log(error))
    return isTracking as boolean;
  }

export async function stopTracking(){
    await Location.stopLocationUpdatesAsync(LOCATION_TASK)
    .catch(error => console.log(error));
}

export async function startTracking() {
    const { status } = await Location.requestBackgroundPermissionsAsync();
    await Location.enableNetworkProviderAsync().then().catch(_ => null);
    
    if (status === 'granted') {
      await Location.startLocationUpdatesAsync(LOCATION_TASK, {
        accuracy: Location.Accuracy.Highest,
        //accuracy: Location.Accuracy.BestForNavigation,
        activityType: 2,
        //em  metros
        deferredUpdatesDistance: 100,
        
        // android behavior
        foregroundService: {
          notificationTitle: 'teste notificacao',
          notificationBody: 'notif body',
          notificationColor: '#333333',
        },
      });
    }
}

/**
 * antigo
 * if (status === 'granted') {
        await Location.startLocationUpdatesAsync(LOCATION_TASK, {
          accuracy: Location.Accuracy.BestForNavigation,
          activityType: 2,
          //em  metros
          deferredUpdatesDistance: 100,
          
          // android behavior
          foregroundService: {
            notificationTitle: 'teste notificacao',
            notificationBody: 'notif body',
            notificationColor: '#333333',
          },
        });
    }
 */