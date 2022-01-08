import React from "react";
import { View, Text, StyleSheet } from "react-native";
import MapView from 'react-native-maps';
import { Marker } from "react-native-maps";
import { Button, FAB, Modal, Portal } from "react-native-paper";
import { mapasProps } from "../../routes/params/AppStackParams";
import { useAuth } from "../../contexts/auth";
import { useInfected } from "../../contexts/infected";

const Mapas: React.FC<mapasProps> = ( {navigation, route} ) =>{

  const [isVisible, setIsVisible] = React.useState(false);
  const { locationTrack } = useInfected();

  function clearStorage(){
    locationTrack.clearTracking()
  }
 
  return (
    <View style={{...StyleSheet.absoluteFillObject}}>
      <MapView style={{flex:1}} >
        {locationTrack.locations.map((local,index) =>
        <Marker key={index} 
        flat={true} 
        coordinate={{latitude:local.coords.latitude, longitude:local.coords.longitude}} 
        /> )
        }
      </MapView>
      <FAB
        style={styles.fab}
        icon="plus"
        onPress={() => { setIsVisible(true) }}
      />
      <Portal>
        <Modal visible={isVisible}
            contentContainerStyle={styles.modalContainer}
            onDismiss={() => {setIsVisible(false)}}>
          <Text style={{fontSize: 50}}>Detalhes</Text>
          <View style={styles.textContainer}>  
          {
            locationTrack.locations.map((location, index) => {return(
              <Text key={index}
              style={{ fontSize: 14 }}>
                Latitude: {location.coords.latitude} Longitude: {location.coords.longitude}
              </Text>     
            )}) 
          }
          </View>
          <View style={styles.buttonContainer}>
            <Button mode="contained" onPress={clearStorage}>Clear Storage</Button>
          </View>
        </Modal>
      </Portal>
    </View>
    
  )
} 
export default Mapas;
//
const styles = StyleSheet.create({
  

  fab: {
    position: 'absolute',
    margin: 24,
    left: 0,
    bottom: 0,
  },
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
  flexDirection: 'column',
  marginVertical: 5, 
  padding: 10, 
  borderRadius: 10, 
  width: '90%'
},
  
});
