import styles from "./Footer.module.css"
import logo from "../../resources/img/Logo.png"
import { Link } from "react-router-dom";

const Footer = () => {
    return (
        <>
            <footer>
                <div className={styles.footerBox}>
                    <Link to={"/"}>
                        <img src={logo} alt="footer_logo" className={styles.imageLogo} /> 
                    </Link>
                    <p className={styles.footerText}>© Recipes 2024</p>
                </div>
            </footer>
        </>
    )
}

export default Footer;