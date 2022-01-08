import { LocationObject } from "expo-location";
import React, { createContext } from "react";
import { useLocationTracking } from "../services/location/useLocation";
import * as TaskManager from "expo-task-manager";
import * as BackgroundFetch from 'expo-background-fetch';
import AsyncStorage from "@react-native-async-storage/async-storage";
import { issick } from "../services/api/DiseaseApi";
import { useAuth } from "../contexts/auth";
import { BACKGROUND_FETCH_TASK } from "../tasks/BackgroundFetch";
interface InfectedContextData {

    locationTrack: {
        locations: LocationObject[];
        isTracking: boolean | undefined;
        startTracking: () => Promise<void>;
        stopTracking: () => Promise<void>;
        clearTracking: () => Promise<void>;
    };
    handleSignOut(): void;
    UserIssick: string | null;

} 

const InfectedContext = createContext<InfectedContextData>({} as InfectedContextData);

const InfectedProvider: React.FC = ({ children }) => {
  
  const { signOut, token } = useAuth() 
  const [UserIssick, setUserIssick] = React.useState<string | null>(null)
  const locationTrack = useLocationTracking();

  React.useEffect( () => {
  const interval = setInterval(async () => {
    let response: string | void
    if(!!token) {
      response = await issick(token);
      (!!response) ? setUserIssick(response) : undefined
    }
  }, 1000) 
  return () => {
      clearInterval(interval)  
  } 
  })  
  React.useEffect( () => {
    locationTrack.startTracking();
  },[])
  

  const handleSignOut = () => {
      if (locationTrack.isTracking) {
        locationTrack.stopTracking()
        .catch( (reason) => console.log(reason) ) ;
      } 
      locationTrack.clearTracking()
      .catch( (reason) => console.log(reason) );
      signOut();
  }

  TaskManager.defineTask(BACKGROUND_FETCH_TASK, async () => {

    //dados da resposta do backend
    //pode ser void ou string
    let response: string | void
    if(!!token) {
      response = await issick(token);
      (!!response) ? setUserIssick(response) : undefined
    }

    
    // return pra onde?
    if(BackgroundFetch.Result.NewData && response) await AsyncStorage.setItem('@Api:issick', response.toString())
    return response ? BackgroundFetch.Result.NewData : BackgroundFetch.Result.NoData;

  });

  return (
  <InfectedContext.Provider value={{ locationTrack, handleSignOut, UserIssick } }>
      {children}
  </InfectedContext.Provider>
  );
};

function useInfected() {

  const context = React.useContext(InfectedContext);

  if (!context) {
    throw new Error('useInfected must be used within an InfectedProvider.');
  }

  return context;
}

export {InfectedProvider, useInfected};
