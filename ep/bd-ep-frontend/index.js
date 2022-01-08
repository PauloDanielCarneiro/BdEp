import 'react-native-gesture-handler';
import { registerRootComponent } from 'expo';

import App from './src/App';
import * as React from 'react';
import { DefaultTheme, Provider as PaperProvider } from 'react-native-paper';

export default function Main() {
  return (
    <PaperProvider theme={DefaultTheme}>
      <App />
    </PaperProvider>
  );
}


registerRootComponent(Main);


// registerRootComponent calls AppRegistry.registerComponent('main', () => App);
// It also ensures that whether you load the app in Expo Go or in a native build,
// the environment is set up appropriately
