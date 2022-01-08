export default function validatePw(pw:string){
    if (!pw) return "Digite uma senha"
    if (pw.length < 6) return 'Use 6 caracteres ou mais para sua senha'
    return ''
}