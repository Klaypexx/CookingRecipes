import styles from "./Preview.module.css"
import headerPreview from "../../resources/img/headerPreview.png"
import plusImg from "../../resources/icons/plus-white.svg"
import Button from "../Button/Button"
import useModalStore from "../../Stores/useModalStore"
import LoginModal from "../Modal/LoginModal"
import { useAuthStore } from "../../Stores/useAuthStore"
import RegisterModal from "../Modal/RegisterModal"

const Preview = () => {
    const {isRegister, isLogin, setLogin} = useModalStore();
    const token = useAuthStore((state) => state.token)
    const handleLogin = () => {
        setLogin(isLogin);
    }

    return (
        <section>
            <div className={styles.previewBlock}>
                <h1 className={styles.headText}>Готовь и делись рецептами</h1>
                <p className={styles.subheadingText}>Никаких кулинарных книг и блокнотов! Храни все любимые рецепты в одном месте.</p>
                <div className={styles.links}>
                    <Button 
                        primary
                        navigation="/" 
                        buttonText="Добавить рецепт">
                        <img src={plusImg} className={styles.plus} />
                    </Button>
                    {token ? null : 
                        <Button 
                            navigation="/"
                            newStyle={{width: "216px"}}
                            buttonText="Войти"
                            onClick={handleLogin}>
                        </Button>
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