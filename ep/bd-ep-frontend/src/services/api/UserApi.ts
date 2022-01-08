import { IGetUserResponse, IUser } from "../../interfaces/user";
import { api } from "./api";

export async function postUser(
    name: string, pw: string, email: string, document: string){
      const response = await api.post( '/User', {
        name:name,
        password:pw,
        email:email,
        document:document
      }, )
      .then(response => {console.log("[api] then post user ", response.status)} )
      .catch(reason => console.log('[api] catch post User', reason.status))
      
    }
  
  export async function postUserLogin( cpf:string, pw:string ) : Promise<IUser | void>{
    const response = await api.post<IUser>('/User/login', {
      document: cpf, password: pw
    }, )
    .then(
      response => {
        console.log('[api] then post User login', response.status)
        return response.data
      })
    .catch(reason => console.log('[api] catch post User login', reason.status))
    
    if(!!response){
      return response
    }
    return undefined;
   
  }

  export async function getUser(user:IUser) : Promise<IUser>{
    
    const response = await api.get<IGetUserResponse>(`/User/${user.document}`, {
      headers:{
        token: user.token
      }
    })
    .then(
      response => {
        console.log('[api] then post User login', response.status)
        user.name = response.data.name
        user.email = response.data.email
        user.id = response.data.id
        return user
      })
    .catch(reason => console.log('[api] catch get User login', reason.status))
    
    return (response) ? response : user;
    
  }