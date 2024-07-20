import BaseModal from "./BaseModal";
import styles from "./AuthModal.module.css"
import LoginModal from "./LoginModal";
import RegisterModal from "./RegisterModal";
import useModalStore from "../../Stores/useModalStore";

const AuthModal = () => {
    const {isAuth, isLogin, isRegister, setAuth, setLogin, setRegister} = useModalStore()
    const handlerLogin = () => {
        setLogin(isLogin);
        setAuth(isAuth);
    }

    const handlerRegister = () => {
        setRegister(isRegister);
        setAuth(isAuth);
    }

    return (
        <>
            <BaseModal haederText="Войдите в профиль" btnPrimaryText="Войти" btnSecondaryText="Регистрация" onClickPrimary={handlerLogin} onClickSecondary={handlerRegister}>
                <p className={styles.authText}>Добавлять рецепты могут только зарегистрированные пользователи.</p>
            </BaseModal>
            
            {isLogin ? 
                <LoginModal />
            : null}

            {isRegister ?
                <RegisterModal />
            
            : null}
        </>
    )
}

export default AuthModal;