import React from 'react';
import { createStackNavigator } from '@react-navigation/stack';

import SignIn from '../pages/login/SignIn';
import SignUp from '../pages/signup/SignUp';
import Home from '../pages/home/Home'
import { AuthStackParamsList } from './params/AuthStackParams';

const AuthStack = createStackNavigator<AuthStackParamsList>()

const AuthRoutes = () => {
    return(
        <AuthStack.Navigator 
        screenOptions={{headerShown:false ,headerMode:"float"}} >
            <AuthStack.Screen name={"Home"} component={Home} />
            <AuthStack.Screen name={"SignIn"} component={SignIn} />
            <AuthStack.Screen name={"SignUp"} component={SignUp} />
            
                
        </AuthStack.Navigator>
        )
}

export default AuthRoutes

//