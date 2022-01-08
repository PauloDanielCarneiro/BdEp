import * as BackgroundFetch from 'expo-background-fetch';

export const BACKGROUND_FETCH_TASK = "background-fetch-issick"

export async function registerBackgroundFetchAsync() {
  return BackgroundFetch.registerTaskAsync(BACKGROUND_FETCH_TASK, {
    minimumInterval:  0.5, // minutes
    
  });
}
export async function unregisterBackgroundFetchAsync() {
  return BackgroundFetch.unregisterTaskAsync(BACKGROUND_FETCH_TASK);
}

export async function getStatusBGAsync(){
  const status = await BackgroundFetch.getStatusAsync()
  return status;
  
}