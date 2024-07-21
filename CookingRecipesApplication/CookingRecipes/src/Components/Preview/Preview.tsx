import styles from "./Preview.module.css"
import headerPreview from "../../resources/img/headerPreview.png"
import plusImg from "../../resources/icons/plus-white.svg"
import BaseLink from "../Link/BaseLink/BaseLink"
import useModalStore from "../../Stores/useModalStore"
import LoginModal from "../Modal/LoginModal/LoginModal"
import RegisterModal from "../Modal/RegisterModal/RegisterModal"
import TokenService from "../../Services/TokenService"

const Preview = () => {
    const {isRegister, isLogin, setLogin} = useModalStore();
    const token = TokenService.getAccessToken();
    const handleLogin = () => {
        setLogin(isLogin);
    }

    return (
        <section>
            <div className={styles.previewBlock}>
                <h1 className={styles.headText}>Готовь и делись рецептами</h1>
                <p className={styles.subheadingText}>Никаких кулинарных книг и блокнотов! Храни все любимые рецепты в одном месте.</p>
                <div className={styles.links}>
                    <BaseLink 
                        primary
                        navigation="/" 
                        linkText="Добавить рецепт">
                        <img src={plusImg} className={styles.plus} />
                    </BaseLink>
                    {token ? null : 
                        <BaseLink 
                            navigation="/"
                            newStyle={{width: "216px"}}
                            linkText="Войти"
                            onClick={handleLogin}>
                        </BaseLink>
                    }
                </div>
            </div>
            <img src={headerPreview} alt="" className={styles.headerPreview}/>

            {isLogin ?
                <LoginModal />  
            :null}

            {isRegister ?
                <RegisterModal />  
            :null}
        </section>
    )
}

export default Preview;