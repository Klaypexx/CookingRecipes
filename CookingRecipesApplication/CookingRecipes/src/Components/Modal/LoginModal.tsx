import AuthService from "../../Services/AuthService";
import { useAuthStore } from "../../Stores/useAuthStore";
import useModalStore from "../../Stores/useModalStore";
import BaseModal from "./BaseModal"

const LoginModal = () => {
    const {unsetAll} = useModalStore();
    const setToken = useAuthStore((state) => state.setToken);
    const handleLogin = async () => {
        const response = await AuthService.login("Sanchez", "12345");
        if (response.status == 200) {
            unsetAll();
            setToken(response.data);
        }
    }

    const handleExit = () => {
        unsetAll();
    }

    return (
        <>
            <BaseModal 
                primary haederText="Войти" 
                hasAccountText="У меня еще нет аккаунта"  
                btnPrimaryText="Войти" 
                btnSecondaryText="Отмена"
                onClickPrimary={handleLogin}
                onClickSecondary={handleExit} >
            </BaseModal>
        </>
    )
}

export default LoginModal;