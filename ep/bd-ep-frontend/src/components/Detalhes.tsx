/* import React from "react";
import { View, Text, StyleSheet } from "react-native";
import { Button, Modal, Portal } from "react-native-paper";
import * as Location from 'expo-location';
import { LocationObject } from "expo-location";

import { detailsProps } from "../routes/params/AppStackParams";
import { useAuth } from "../contexts/auth";

import { useLocationTracking } from "../services/location/useLocation";
import { clearLocations, getLocations } from "../services/location/storeLocation";




const Details: React.FC<detailsProps> = ({navigation, route}) =>{
  
  const { signOut } = useAuth()
  const [locations, setLocations] = React.useState<LocationObject[]>([]);

  const [isVisible, setIsVisible] = React.useState(navigation.isFocused);
  
  const locationTrack = useLocationTracking();
  
  React.useEffect( () => {
    atualizarTexto()
  }) 

  function handleSignOut() {
    if (locationTrack.isTracking) {
      locationTrack.stopTracking();
      locationTrack.clearTracking();
    } 
    signOut()
  }
  async function clearStorage(){
    await clearLocations()
  }

  const dismiss = () => {
    setIsVisible(false)
    navigation.navigate("Mapas") 
  }
  const atualizarTexto = async () => {
    setLocations( await getLocations() )
  } 
  return (
    <Portal>
      <Modal visible={isVisible}
          contentContainerStyle={styles.modalContainer}
          onDismiss={dismiss}>
        <Text style={{fontSize: 50}}>Detalhes</Text>
        <View style={styles.textContainer}>
        
        {(locations) ? 
          locations.map((location, index) => {
            <Text key={index}
            style={{ fontSize: 16, fontWeight: "bold" }}>
              Latitude: {location.coords.latitude} 
              Longitude: {location.coords.longitude}
            </Text>     
          }) : <></>
        }
        
        </View>
          

        
        <View style={styles.buttonContainer}>
          <Button mode="contained" onPress={handleSignOut}>Sign out</Button>
          <Button mode="contained" onPress={clearStorage}>Clear Storage</Button>
        </View>
      </Modal>
    </Portal>
  )
}

const styles = StyleSheet.create({
    modalContainer: {
        borderRadius: 18, 
        backgroundColor:"white",
        justifyContent: "center",
        marginHorizontal: 16,
        alignItems:"center"
      },
      buttonContainer: {
        marginVertical: 5, 
        padding: 10, 
        borderRadius: 10, 
        flexDirection: "row", 
      justifyContent:"center"
    },
    textContainer:{
      marginVertical: 5, 
      padding: 10, 
      borderRadius: 10, 
      width: '40%'
    },
      
})

export default Details;
//antigo
/* return (
  <Portal>
    <Modal visible={isVisible}
        contentContainerStyle={styles.modalContainer}
        onDismiss={() => {navigation.navigate("Mapas", {locations: location} ); setIsVisible(false);}}>
      <Text style={{fontSize: 50}}>DETALHES</Text>
      <View style={{marginVertical: 5, padding: 10, borderRadius: 10, width: '40%'}}>
          
      </View>
        <Text style={{ fontSize:16, fontWeight:"bold" }}>Latitude: {location?.coords.latitude} </Text>
        <Text style={{ fontSize:16, fontWeight:"bold" }}>Longitude: {location?.coords.longitude} </Text>
      {/* <View style={{marginVertical: 5, padding: 10, borderRadius: 10,}}>
        <Button mode="contained" onPress={requestPermission}>Start background update</Button>
      </View>
        <Text style={{ fontSize:16, fontWeight:"bold" }}>BGlatitude: {locationBG?.coords.latitude} </Text>
        <Text style={{ fontSize:16, fontWeight:"bold" }}>BGlongitude: {locationBG?.coords.longitude} </Text> }
      <View style={{marginVertical: 5, padding: 10, borderRadius: 10, flexDirection: "row", justifyContent:"center"}}>
        <Button mode="contained" onPress={handleSignOut}>Sign out</Button>
        <Button mode="contained" onPress={clearStorage}>Clear Storage</Button>
      </View>
    </Modal>
  </Portal>
)
 */ 