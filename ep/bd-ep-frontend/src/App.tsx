import React from 'react';
import 'react-native-gesture-handler';
import { NavigationContainer } from '@react-navigation/native';
import {  SafeAreaView, StyleSheet,  } from 'react-native';

import Routes from './routes/index';
import { AuthProvider } from "./contexts/auth";

export default function App() {
  //NOTE: SafeAreaView nao compativel com android?

  return (
    <SafeAreaView style={styles.container}>
      <NavigationContainer>
        <AuthProvider>
          <Routes />
        </AuthProvider>
      </NavigationContainer>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
  }
  
 
});

