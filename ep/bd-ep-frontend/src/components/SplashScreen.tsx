import React from "react";
import { View, Text } from "react-native";
import { Card, Portal } from "react-native-paper";

export function SplashScreen() {
    return (

      <Portal>
        <Card style={{backgroundColor:"black", flexWrap:'wrap',}}>
          <Text style={{color:"white",alignSelf:'auto', fontSize:50}}>Loading...</Text>
        </Card>
      </Portal>

    );
  }