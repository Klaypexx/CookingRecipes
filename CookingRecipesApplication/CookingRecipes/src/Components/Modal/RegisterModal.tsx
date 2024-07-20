import useModalStore from "../../Stores/useModalStore";
import BaseModal from "./BaseModal"

const RegisterModal = () => {
    const {unsetAll} = useModalStore();
    const handleExit = () => {
        unsetAll();
    }
    return (
        <>
            <BaseModal 
                primary 
                haederText="Регистрация" 
                hasAccountText="У меня уже есть аккаунт" 
                btnPrimaryText="Зарегестрироваться" 
                btnSecondaryText="Отмена"
                onClickSecondary={handleExit} >
            </BaseModal>
        </>
    )
}

export default RegisterModal;