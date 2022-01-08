import React from 'react';
import { AppStackParamsList } from './params/AppStackParams';
import Mapas from '../pages/map/Map';
import { createBottomTabNavigator } from '@react-navigation/bottom-tabs';
import StatusPage from '../pages/status';
import Profile from '../pages/profile';
import AntIcon from 'react-native-vector-icons/AntDesign';
import FeatherIcon from 'react-native-vector-icons/Feather'
 
//const AppStack = createStackNavigator<AppStackParamsList>();
const AppStack = createBottomTabNavigator<AppStackParamsList>();

const AppRoutes: React.FC = () => (
  
  <AppStack.Navigator 
  screenOptions={{headerShown: true}} >

    <AppStack.Group >
      <AppStack.Screen name={"Mapas"} component={Mapas} 
      options={{
        tabBarLabel: 'Map',
        tabBarIcon: ({ color, size }) => (
          <FeatherIcon name="map" color={color} size={size} />
          ), 
        }}  /> 
      <AppStack.Screen name={'Status'} component={StatusPage} 
      options={{
        tabBarLabel: 'Status',
        tabBarIcon: ({ color, size }) => (
          <AntIcon name="user" color={color} size={size} />
          ), 
        }} /> 
      <AppStack.Screen name={"Profile"} component={Profile} 
      options={{
        tabBarLabel: 'Profile',
        tabBarIcon: ({ color, size }) => (
          <AntIcon name="profile" color={color} size={size} />
          ), 
        }} />
      </AppStack.Group>



    {/* <AppStack.Screen name="Details" 
    component={Details} 
    initialParams={{showModal:false}} 
    options={{presentation: "transparentModal"}} /> */}

  </AppStack.Navigator>
);

export default AppRoutes;
