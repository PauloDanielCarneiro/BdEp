export default function validateEmail(email:string) 
{
    var re = /\S+@\S+\.\S+/;
    
    if (!email) return "Digite um e-mail"
    if (!re.test(email)) return 'Digite um e-mail válido'
    return ''
}