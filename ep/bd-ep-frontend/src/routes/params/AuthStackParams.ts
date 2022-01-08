import { StackNavigationProp } from "@react-navigation/stack";

export type AuthStackParamsList = {
    SignIn: {showModal:boolean} | undefined;
    SignUp: {showModal:boolean} | undefined;
    Home: undefined;
}

type signInProp = StackNavigationProp<AuthStackParamsList, 'SignIn'>;
type signUpProp = StackNavigationProp<AuthStackParamsList, 'SignUp'>;
type homeProp = StackNavigationProp<AuthStackParamsList, 'Home'>;

export type {signInProp};
export type {signUpProp};
export type {homeProp};
