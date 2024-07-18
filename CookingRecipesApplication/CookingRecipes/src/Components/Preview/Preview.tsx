import styles from "./Preview.module.css"
import headerPreview from "../../resources/img/headerPreview.png"
import plusImg from "../../resources/icons/plus-white.svg"
import { Link } from "react-router-dom"

const Preview = () => {
    return (
        <section>
            <div className={styles.previewBlock}>
                <h3 className={styles.headText}>Готовь и делись рецептами</h3>
                <p className={styles.subheadingText}>Никаких кулинарных книг и блокнотов! Храни все любимые рецепты в одном месте.</p>
                <div className={styles.links}>
                    <Link to={"/"} className={styles.newRecipeButton} style={{ backgroundColor: 'rgb(253, 177, 0)'}}>
                        <img src={plusImg} className={styles.plus} />
                        <p className={styles.newRecipeText}>Добавить рецпт</p>
                    </Link>
                    <Link to={"/"} className={styles.signUpButton}>
                        <p className={styles.signUpText}>Войти</p>
                    </Link>
                </div>
            </div>
            <img src={headerPreview} alt="" className={styles.headerPreview}/>
        </section>
    )
}

export default Preview;