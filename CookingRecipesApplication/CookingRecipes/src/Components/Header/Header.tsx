import styles from './Header.module.css';
import logo from '../../resources/img/Logo.png';
import userIcon from '../../resources/icons/user.svg';
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
        const result = await UserService.username();
        if (result.response && result.response.status === 200) {
          setUserName(result.response.data.userName);
        }
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
      <div className={styles.container}>
        <div className={styles.navigation}>
          <Link to={'/'}>
            <img src={logo} alt="header_logo" className={styles.imageLogo} />
          </Link>
          <div className={styles.containerButtons}>
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
          </div>
        </div>

        <div className={styles.authContainer}>
          <div className={styles.userAvatar}>
            <img src={userIcon} alt="User Icon" className={styles.userAvatarImg} />
          </div>
          {isAuthorized && userName ? (
            <div className={styles.authBlock}>
              <p className={styles.authText}>Привет, {userName}</p>
              <div className={styles.line}></div>
              <img src={exitIcon} alt="" className={styles.exit} onClick={handleLogout} />
            </div>
          ) : (
            <p className={styles.avatarText} onClick={handleLogin}>
              Войти
            </p>
          )}
        </div>
      </div>
    </header>
  );
};

export default Header;
