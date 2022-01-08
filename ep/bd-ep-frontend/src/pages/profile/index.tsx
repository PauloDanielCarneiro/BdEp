import React from "react";
import { View, Text, StyleSheet,  } from 'react-native'
import { Button, Checkbox,  Modal, Portal, TextInput} from "react-native-paper";
import { useInfected } from "../../contexts/infected";
import { useAuth } from "../../contexts/auth";
import DateTimePicker from '@react-native-community/datetimepicker';
import DropDownPicker, { ItemType, ValueType } from 'react-native-dropdown-picker';
import { /* getDiseases,*/ getUserDiseases,  postUserDisease, patchUserDisease } from "../../services/api/DiseaseApi";


export default function Profile() {
    
    const { handleSignOut } = useInfected();
    const { token, user } = useAuth()

    const [isVisibleAddDisease, setIsVisibleAddDisease] = React.useState<boolean>(false)
    const [isVisibleEditDisease, setIsVisibleEditDisease] = React.useState<boolean>(false)

    //dropdown Add disease
    const [showDropDownAdd, setShowDropDownAdd] = React.useState(false);
    //const [diseases, setDiseases] = React.useState<ItemType[]>([])
    const [valueAdd, setValueAdd] = React.useState<ValueType | null>(null);
    
    //dropdown edit disease
    const [showDropDownEdit, setShowDropDownEdit] = React.useState(false);
    const [userDiseases, setUserDiseases] = React.useState<ItemType[]>([])
    const [valueEdit, setValueEdit] = React.useState<ValueType | null>(null);

    //checkbox for add disease
    const [cured, setCured] = React.useState<boolean>(false)
    const [symptoms, setSymptoms] = React.useState<boolean>(false)
    
    //checkbox for edit disease
    const [curedEdit, setCuredEdit] = React.useState<boolean>(false)
    const [symptomsEdit, setSymptomsEdit] = React.useState<boolean>(false)

    //datepicker add disease
    const [startDate, setStartDate] = React.useState<Date>( new Date(Date.now()) );
    const [showStartDatePicker, setShowStartDatePicker] = React.useState<boolean>(false);

    //datepicker add disease
    const [endDate, setEndDate] = React.useState<Date>( new Date(Date.now()) );
    const [showEndDatePicker, setShowEndDatePicker] = React.useState<boolean>(false);

    //datepicker edit disease
    const [endDateEdit, setEndDateEdit] = React.useState<Date>( new Date(Date.now()) );
    const [showEndDatePickerEdit, setShowEndDatePickerEdit] = React.useState<boolean>(false);
    
    const [items, setItems] = React.useState([
        {label: 'Covid', value: 'Covid'},
    ]);
  
    const onChangeStartDate = (event: any, date?:Date | undefined): void => {
        const currentDate = date || startDate;
        setShowStartDatePicker(false)
        console.log(currentDate.toISOString())
        setStartDate(currentDate);
    };

    const onChangeEndDate = (event: any, date?:Date | undefined): void => {
        const currentDate = date || endDate;
        setShowEndDatePicker(false)
        //console.log(currentDate.toISOString())
        setEndDate(currentDate);
    };  
    const onChangeEndDateEdit = (event: any, date?:Date | undefined): void => {
        const currentDate = date || endDate;
        setShowEndDatePickerEdit(false)
        //console.log(currentDate.toISOString())
        setEndDateEdit(currentDate);
    };

    function cleanFields() {
    setIsVisibleAddDisease(false)
    setIsVisibleEditDisease(false)
    setShowDropDownAdd(false);
    setValueAdd(null);
    setShowDropDownEdit(false);
    setValueEdit(null);
    setCured(false)
    setSymptoms(false)
    setCuredEdit(false)
    setSymptomsEdit(false)
    setShowStartDatePicker(false);
    setStartDate( new Date(Date.now()) );
    setEndDate( new Date(Date.now()) );
    setShowEndDatePicker(false);
    setEndDateEdit( new Date(Date.now()) );
    setShowEndDatePickerEdit(false);
    }

    const onSubmitAddDisease = async () =>{
        //0001-01-01T12:00:00Z
        if(!!token && !!valueAdd) {
            await postUserDisease(token, 
                valueAdd as string, 
                cured, 
                symptoms, 
                startDate.toISOString(), 
                (cured)? endDate.toISOString() : "0001-01-01T12:00:00Z")
                cleanFields()
        }else {
            console.log("submitAddDisease" ,token, valueAdd)
        } 
    }

    const onSubmitEditDisease = async () => {
        if(!!token && !!valueEdit) {
            await patchUserDisease(token, 
                valueEdit as string, 
                curedEdit, 
                symptomsEdit, 
                (cured)? endDateEdit.toISOString() : "0001-01-01T12:00:00Z")
                cleanFields()
        }else {
            console.log("submitAddDisease" ,token, valueEdit)
        } 
    }
  
    React.useEffect( () => {
        //get diseases for dropdown
        /* async function getDiseaseNames(){
            let arr = [] as ItemType[]
            if(!!token) {
                arr = await getDiseases(token)
            } else 
                console.log( 'token nao existente. Page profile');
            setDiseases(arr)     
        } */
        async function getUserDiseaseNames(){
            let arr: ItemType[] | void
            if(!!token) {
                arr = await getUserDiseases(token);
                (!!arr) ? setUserDiseases(arr) : arr=undefined    ; 

            } else 
                console.log( 'token nao existente. Page profile');
        } 
        //getDiseaseNames()
        getUserDiseaseNames() 
    },[])

    return (
        <View style={styles.container}>
            <View style={styles.textContainer}>
                <View style={styles.TextContainer}>
                    <Text style={styles.textLeft}>Nome:</Text>
                    <Text style={styles.textRight}>{(user) ? user.name: 'Name batata'}</Text>

                </View>
                <View style={styles.TextContainer}>
                    <Text style={styles.textLeft}>CPF:</Text>
                    <Text style={styles.textRight}>{(user) ? user.document: 'CPF noto gibben'}</Text>

                </View>
                <View style={styles.TextContainer}>
                    <Text style={styles.textLeft}>Email:</Text>
                    <Text style={styles.textRight}>{(user) ? user.email: 'email not given'}</Text>

                </View>
            </View>
            
            <View style={styles.buttonContainer}>
                <Button style={styles.buttonStyle} 
                mode="outlined" 
                color="red" 
                contentStyle={{height:50}}
                onPress={() => setIsVisibleAddDisease(true)}>
                Add Disease
                </Button>
                <Button style={styles.buttonStyle} 
                mode="outlined" 
                color="blue" 
                contentStyle={{height:50,}}
                onPress={() => setIsVisibleEditDisease(true)}>
                Edit Disease
                </Button>
                
            </View>
            <View style={{ marginHorizontal: "12.5%",}}>
                    <Button style={styles.buttonStyle} 
                    mode='contained' 
                    color="red" 
                    contentStyle={{height:50,}}
                    
                    onPress={handleSignOut}>
                    Sign out
                    </Button>

            </View>
           

            <Portal>
                <Modal visible={isVisibleAddDisease}
                contentContainerStyle={styles.modalContainer}
                onDismiss={() => {cleanFields()}}>
                    
                <View style={styles.formContainer}>
                    <View style={styles.InputContainer}>
                        <Text style={{  alignSelf:'center',width:"30%", marginHorizontal:'5%'}}>Disease name</Text>
                        <DropDownPicker
                        open={showDropDownAdd}
                        setOpen={setShowDropDownAdd}
                        value={valueAdd}
                        items={items}
                        setValue={setValueAdd}
                        setItems={setItems}
                        //items={userDiseases}
                        //setItems={setUserDisease}
                        placeholder="Disease"
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
                    {cured && (
                    <View style={styles.InputContainer}>
                        <Text style={{ alignSelf:'center',width:"40%", marginHorizontal:'5%'}}>When did you get treated?</Text>
                        <Button 
                        onPress={() => setShowEndDatePicker(true)} 
                        mode='outlined' 
                        color="black"
                        style={{alignSelf:'center', height:"100%", width:'50%', borderColor:'white'}}
                        >
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
                    onPress={onSubmitAddDisease}>
                        Submit
                    </Button>
                </View>
                </Modal>

                <Modal visible={isVisibleEditDisease}
                contentContainerStyle={styles.modalContainer}
                onDismiss={() => {cleanFields()}}>
                    
                <View style={styles.formContainer}>
                <View style={styles.InputContainer}>
                        <Text style={{  alignSelf:'center',width:"30%", marginHorizontal:'5%'}}>Disease name</Text>
                        <DropDownPicker
                        open={showDropDownEdit}
                        items={userDiseases}
                        setItems={setUserDiseases}
                        value={valueEdit}
                        setOpen={setShowDropDownEdit}
                        setValue={setValueEdit}
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
                        status={curedEdit ? 'checked' : 'unchecked'}
                        onPress={() => {
                            setCuredEdit(!curedEdit);
                        }}/>

                    </View>
                    <View style={styles.InputContainer}>
                        <Text style={{  alignSelf:'center',width:"40%", marginHorizontal:'5%'}}>Are you showing the symptoms?</Text>
                        <Checkbox
                        status={symptomsEdit ? 'checked' : 'unchecked'}
                        onPress={() => {
                            setSymptomsEdit(!symptomsEdit);
                        }}/>

                    </View>

                    
                    {curedEdit && (
                    <View style={styles.InputContainer}>
                        <Text style={{ alignSelf:'center',width:"40%", marginHorizontal:'5%'}}>When did you get treated?</Text>

                        <Button onPress={() => setShowEndDatePickerEdit(true)} 
                        mode='outlined' 
                        color="black"
                        style={{alignSelf:'center', height:"100%", width:'50%', borderColor:'white'}}
                        >
                        {endDateEdit.toDateString()}
                        </Button>

                        {showEndDatePickerEdit &&  (
                        <DateTimePicker
                        testID="dateEndTimePickerEdit"
                        value={endDateEdit}
                        display="default"
                        onChange={onChangeEndDateEdit}
                        />
                        )}
                    </View>)}            

                   
                </View> 
                <View style={styles.buttonContainer}>
                    <Button style={styles.buttonStyle} color="red" 
                    mode="contained" 
                    onPress={onSubmitEditDisease}>
                        Submit
                    </Button>
                </View>
                </Modal>
            </Portal>

            
        </View>
    )
}

const styles = StyleSheet.create({
    container:{
        ...StyleSheet.absoluteFillObject,
        justifyContent:"center",
    },
    modalContainer: {
        borderRadius: 18, 
        backgroundColor:"white",
        marginHorizontal: 16,
        justifyContent:"space-evenly",
        alignContent:'center',
        overflow:'hidden',

    },
    formContainer:{
        padding: 5,
        margin: 5,
    },
    InputContainer:{
        position:'relative',
        height: 40,
        margin:5,
        flexDirection:"row",
        
    },
    buttonContainer:{
        alignSelf:"center",
        flexDirection: "row",
    },
    buttonStyle:{
        borderWidth:1,
        margin:10,
               
    },
    textContainer:{
        borderWidth:1,
        borderColor:'grey',
        borderRadius:8,
        marginHorizontal:"10%",
        padding:5,
        marginBottom: 30,
        position: "relative",
    },
    textLeft:{  
        fontSize:20,
        width:"20%",
        maxWidth:"30%",
    }, 
    textRight: {
        fontSize:18,
        textAlign:'right',
        maxWidth:"72%",
    },
    TextContainer:{
        width:"100%",
        position:'relative',
        flexDirection:'row',
        marginVertical: 5
    }

})