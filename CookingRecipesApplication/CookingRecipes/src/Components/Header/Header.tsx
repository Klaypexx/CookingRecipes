import styles from "./Header.module.css"
import logo from "../../resources/img/Logo.png"
import userIcon from "../../resources/icons/user.svg"
import exitIcon from "../../resources/icons/exit.svg"
import { NavLink } from "react-router-dom"
import classNames from "classnames"
import { useEffect, useState } from "react"
import UserService from "../../Services/UserService"
import AuthModal from "../Modal/AuthModal/AuthModal"
import useModalStore from "../../Stores/useModalStore"
import LogoutModal from "../Modal/LogoutModal/LogoutModal"
import TokenService from "../../Services/TokenService"

const Header = () => {
    const [userName, setUserName] = useState('');
    const {isAuth, isLogout, setAuth, setLogout} = useModalStore();
    const token = TokenService.getAccessToken();
    
    useEffect(() => {
        const fetchUsername = async () => {
            if (token) {
                try {
                    const response = await UserService.username();
                    const { userName } = response; 
                    setUserName(userName);
                } catch (error) {
                    console.error("Ошибка при получении имени пользователя:", error);
                }
            }
        };
    
        fetchUsername();
    }, [token]);

    const handleLogin = () => {
        setAuth(isAuth);
    }

    const handleLogout = () => {
        setLogout(isLogout);
    }

    return (
        <header>
          <div className={styles.container}>
            <div className={styles.navigation}>
              <img src={logo} alt="header_logo" className={styles.imageLogo} /> 
              <div className={styles.containerButtons}>
                <NavLink className={({isActive}) => 
                    classNames(
                        styles.headerText,
                        {[styles.headerTextActive]: isActive}
                    )
                 }
                    to={'/'}
                >
                Главная
                </NavLink>
                <NavLink className={({isActive}) => 
                    classNames(
                        styles.headerText,
                        {[styles.headerTextActive]: isActive}
                    )
                 }
                    to={'/'}
                >
                Рецепты
                </NavLink>
                <NavLink className={({isActive}) => 
                    classNames(
                        styles.headerText,
                        {[styles.headerTextActive]: isActive}
                    )
                 }
                    to={'/secret'}
                >
                Избранное
                </NavLink>
              </div>
            </div>

            <div className={styles.authContainer}>
                <div className={styles.userAvatar}>
                    <img src={userIcon} alt="User Icon" className={styles.userAvatarImg}/>
                </div>
                {userName ? 
                    (
                        <div className={styles.authBlock}>
                            <p className={styles.authText}>Привет, {userName}</p>
                            <div className={styles.line}></div>
                            <img src={exitIcon} alt="" className={styles.exit} onClick={handleLogout}/>
                        </div>
                    )
                    :
                    <p className={styles.avatarText} onClick={handleLogin}>Войти</p>
                }
            </div>

          </div>
          {isAuth ? 
            <AuthModal />
          :null}

          {isLogout ?
            <LogoutModal />
          :null}
        </header>
    )
}

export default Header