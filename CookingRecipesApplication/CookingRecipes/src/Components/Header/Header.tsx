import styles from "./Header.module.css"
import logo from "../../resources/img/Logo.png"
import userIcon from "../../resources/icons/user.svg"
import exitIcon from "../../resources/icons/exit.svg"
import { NavLink, useNavigate } from "react-router-dom"
import classNames from "classnames"
import { useAuthStore } from "../../Stores/useAuthStore"
import { useEffect, useState } from "react"
import UserService from "../../Services/UserService"
import AuthService from "../../Services/AuthService"

const Header = () => {
    const [userName, setUserName] = useState('');
    const token = useAuthStore((state) => state.token);
    const navigate = useNavigate();

    useEffect(() => {
        if (token) {
            const getUsername = async () => {
                const response = await UserService.username();
                const { userName } = response; 
                setUserName(userName);
            }
            getUsername();
        }
    }, [token])

    const handleLogin = async () => {
        await AuthService.login('Дмитрий', '12345')
        navigate(0);
    }

    const handleLogout = async () => {
        await AuthService.logout();
        navigate(0);
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
                    to={'/'}
                >
                Избранное
                </NavLink>
              </div>
            </div>

            <div className={styles.avatar}>
                <div className={styles.userAvatar}>
                    <img src={userIcon} alt="User Icon" className={styles.userAvatarImg}/>
                </div>
                {token ? 
                    (
                        <div className={styles.authContainer}>
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
        </header>
    )
}

export default Header