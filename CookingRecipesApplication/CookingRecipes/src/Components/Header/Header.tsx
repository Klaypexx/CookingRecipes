import styles from './Header.module.css';
import logoImage from '../../resources/img/Logo.png';
import userAvatar from '../../resources/icons/user.svg';
import exitIcon from '../../resources/icons/exit.svg';
import { Link, NavLink } from 'react-router-dom';
import classNames from 'classnames';
import { useEffect, useState } from 'react';
import UserService from '../../Services/UserService';
import useModalStore from '../../Stores/useModalStore';
import useAuthStore from '../../Stores/useAuthStore';

const Header = () => {
  const [userName, setUserName] = useState<string>();
  const { isAuth, isLogout, setAuth, setLogout } = useModalStore();
  const { isAuthorized } = useAuthStore();

  useEffect(() => {
    if (isAuthorized) {
      const fetchUsername = async () => {
        await UserService.username().then((res) => {
          if (res) {
            setUserName(res.response.data.userName);
          }
        });
      };
      fetchUsername();
    }
  }, [isAuthorized]);

  const handleLogin = () => {
    setAuth(isAuth);
  };

  const handleLogout = () => {
    setLogout(isLogout);
  };

  return (
    <header>
      <div className={styles.navigation}>
        <Link to={'/'}>
          <img src={logoImage} alt="logoImage" className={styles.logoImage} />
        </Link>
        <nav className={styles.linksFlex}>
          <NavLink
            className={({ isActive }) => classNames(styles.headerText, { [styles.headerTextActive]: isActive })}
            to={'/'}
          >
            Главная
          </NavLink>
          <NavLink
            className={({ isActive }) => classNames(styles.headerText, { [styles.headerTextActive]: isActive })}
            to={'/recipes'}
          >
            Рецепты
          </NavLink>
          <NavLink
            className={({ isActive }) => classNames(styles.headerText, { [styles.headerTextActive]: isActive })}
            to={'/favourites'}
            state={{ from: location.pathname }}
          >
            Избранное
          </NavLink>
        </nav>
      </div>

      <div className={styles.authFlex}>
        <div className={styles.userAvatarBox}>
          <img src={userAvatar} alt="User Icon" className={styles.userAvatarImage} />
        </div>
        {isAuthorized && userName ? (
          <div className={styles.authBox}>
            <p className={styles.authText}>Привет, {userName}</p>
            <div className={styles.line}></div>
            <img src={exitIcon} alt="" className={styles.exit} onClick={handleLogout} />
          </div>
        ) : (
          <p className={styles.authText} onClick={handleLogin}>
            Войти
          </p>
        )}
      </div>
    </header>
  );
};

export default Header;
