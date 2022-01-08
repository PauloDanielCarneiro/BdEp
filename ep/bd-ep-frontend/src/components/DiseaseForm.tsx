/* import React from "react";
import { View, Text, StyleSheet } from "react-native";
import { Button, Checkbox, Modal } from "react-native-paper";
import DateTimePicker from '@react-native-community/datetimepicker';

import DropDownPicker, { ValueType } from "react-native-dropdown-picker";
import { useNavigation } from "@react-navigation/native";

interface props{
  diseases: {
    label: string;
    value: string;
  };
}


const DiseaseForm: React.FC<props> = (props) =>{
  
  const navigation = useNavigation()
  const [isVisible, setIsVisible] = React.useState<boolean>(false)
  const [disease, setDisease] = React.useState<object | null>()

    const [cured, setCured] = React.useState<boolean>(false)
    const [symptoms, setSymptoms] = React.useState<boolean>(false)

    const [startDate, setStartDate] = React.useState<Date>( new Date(Date.now()) );
    const [showStartDatePicker, setShowStartDatePicker] = React.useState<boolean>(false);

    const [endDate, setEndDate] = React.useState<Date>( new Date(Date.now()) );
    const [showEndDatePicker, setShowEndDatePicker] = React.useState<boolean>(false);

    const [showDropDown, setShowDropDown] = React.useState(false);
    const [value, setValue] = React.useState<ValueType | null>(null);
    const [items, setItems] = React.useState([
        {label: 'Apple', value: 'apple'},
        {label: 'Banana', value: 'banana'}
    ]);
  
    const onChangeStartDate = (event: any, date?:Date | undefined): void => {
        const currentDate = date || startDate;
        console.log(currentDate.toISOString())
        setStartDate(currentDate);
        setShowStartDatePicker(false)
    };

    const onChangeEndDate = (event: any, date?:Date | undefined): void => {
        const currentDate = date || endDate;
        console.log(currentDate.toISOString())
        setEndDate(currentDate);
        setShowEndDatePicker(false)
    };

    const onSubmit = () => {
      
    }
  
  return (
    <>
      <View style={styles.formContainer}>
          <View style={styles.InputContainer}>
              <Text style={{  alignSelf:'center',width:"30%", marginHorizontal:'5%'}}>Disease name</Text>
              <DropDownPicker
              open={showDropDown}
              value={value}
              items={items}
              setOpen={setShowDropDown}
              setValue={setValue}
              setItems={setItems}
              placeholder="Disease"
              zIndex={10}
              listMode="MODAL"
              style={{ width:'60%', height:"100%", }}
              dropDownContainerStyle={{ width:'60%',}}
              />

          </View>
          <View style={styles.InputContainer}>
              <Text style={{  alignSelf:'center',width:"40%", marginHorizontal:'5%'}}>Was the disease treated?</Text>
              <Checkbox
              status={cured ? 'checked' : 'unchecked'}
              onPress={() => {
                  setCured(!cured);
              }}/>

          </View>
          <View style={styles.InputContainer}>
              <Text style={{  alignSelf:'center',width:"40%", marginHorizontal:'5%'}}>Are you showing the symptoms?</Text>
              <Checkbox
              status={symptoms ? 'checked' : 'unchecked'}
              onPress={() => {
                  setSymptoms(!symptoms);
              }}/>

          </View>

          <View style={styles.InputContainer}>
              <Text style={{  alignSelf:'center',width:"40%", marginHorizontal:'5%'}}>Around when did you get infected?</Text>

              <Button onPress={() => setShowStartDatePicker(true)} mode='outlined' 
              color="black"
              style={{alignSelf:'center', height:"100%", width:'50%', borderColor:'white'}}>
              {startDate.toDateString()}
              </Button>
              {showStartDatePicker && (
              <DateTimePicker
              testID="dateStartTimePicker"
              value={startDate}
              display="default"
              onChange={onChangeStartDate}
              />
              )}
          </View>   
          {cured && (<View style={styles.InputContainer}>
              <Text style={{ alignSelf:'center',width:"40%", marginHorizontal:'5%'}}>When did you get treated?</Text>

              <Button onPress={() => setShowEndDatePicker(true)} mode='outlined' 
              color="black"
              style={{alignSelf:'center', height:"100%", width:'50%', borderColor:'white'}}>
              {endDate.toDateString()}
              </Button>
              {showEndDatePicker &&  (
              <DateTimePicker
              testID="dateEndTimePicker"
              value={endDate}
              display="default"
              onChange={onChangeEndDate}
              />
              )}
          </View>)}            

        
      </View> 
      <View style={styles.buttonContainer}>
          
          <Button style={styles.buttonStyle} color="red" 
          mode="contained" 
          onPress={onSubmit}>
              Submit
          </Button>
      </View>
    </>
  )
}

const styles = StyleSheet.create({
  container:{
      ...StyleSheet.absoluteFillObject,
      justifyContent:"center",
  },
  modalContainer: {
      borderRadius: 18, 
      backgroundColor:"blue",
      marginHorizontal: 16,
      justifyContent:"space-evenly",
      alignContent:'center',
      overflow:'hidden',

  },
  formContainer:{
      backgroundColor:"green",
      padding: 5,
      margin: 5,
  },
  InputContainer:{
      position:'relative',
      height: 40,
      margin:5,
      backgroundColor:"red",
      flexDirection:"row",
      zIndex:-1
      
  },
  
  buttonContainer:{
      flexDirection: "row",
      alignSelf:"center"
  },
  buttonStyle:{
      margin: 10,
      padding: 10,
  },


})

export default DiseaseForm;
 */