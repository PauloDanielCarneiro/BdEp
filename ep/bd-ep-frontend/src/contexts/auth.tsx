import AsyncStorage from "@react-native-async-storage/async-storage";
import * as TaskManager from "expo-task-manager";
import React, { createContext, useState } from "react";
import { IUser } from "../interfaces/user";
import { getUser, postUserLogin } from "../services/api/UserApi";
import { registerBackgroundFetchAsync } from "../tasks/BackgroundFetch";


interface AuthContextData {
  signed: boolean;
  user: IUser | null;
  isLoading: boolean;
  signIn(cpf:string, pw:string): Promise<void>;
  signOut(): void;
  token: string | null;
} 

const AuthContext = createContext<AuthContextData>({} as AuthContextData);

const AuthProvider: React.FC = ({ children }) => {
  
  const [user, setUser] = useState<IUser | null>(null);
  const [isLoading, setIsLoading] = useState(true);
  const [token, setToken] = useState<string | null>(null);

  async function signIn(cpf:string, pw:string) {
    
    let response = await postUserLogin(cpf, pw);
    //console.log( "login response", response )
    
    if(response) {
      
      let userResponse = await getUser(response)
      
      await AsyncStorage.setItem('@Auth:user', JSON.stringify(response))
      setUser( userResponse )
      setToken(userResponse.token)
      //registerBackgroundFetchAsync()
    } 
  }

  async function signOut() {
    await AsyncStorage.clear();
    TaskManager.unregisterAllTasksAsync();
    setUser(null);
  }

 React.useEffect(() => {

    async function loadStorageData() {
      
      const storagedUser = await AsyncStorage.getItem('@Auth:user');
      console.log('storagedUser', storagedUser)
      if (storagedUser ) {
        const usuario = await JSON.parse(storagedUser) as IUser
        console.log('usuario', usuario)

        setUser(usuario);
        setToken(usuario.token)
        //console.log(`token ${storagedToken}`)
        console.log( "login token", usuario.token )
        
      }
      setIsLoading(false)

      
    }
    
    loadStorageData();
  

  },[]); 
  
  return (
    <AuthContext.Provider value={{ signed: !!user, user, isLoading, signIn, signOut, token }}>
      {children}
    </AuthContext.Provider>
  );
};

function useAuth() {

  const context = React.useContext(AuthContext);

  if (!context) {
    throw new Error('useAuth must be used within an AuthProvider.');
  }

  return context;
}

export {AuthProvider, useAuth};
