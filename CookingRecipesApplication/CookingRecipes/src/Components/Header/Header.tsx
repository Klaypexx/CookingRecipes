import classNames from 'classnames';
import { useEffect, useState } from 'react';
import { Link, NavLink } from 'react-router-dom';
import exitIcon from '../../resources/icons/exit.svg';
import userAvatar from '../../resources/icons/user.svg';
import logoImage from '../../resources/img/Logo.png';
import UserService from '../../Services/UserService';
import useAuthStore from '../../Stores/useAuthStore';
import useModalStore from '../../Stores/useModalStore';
import useUserStore from '../../Stores/useUserStore';
import styles from './Header.module.css';

const Header = () => {
  const [nameOfUser, setNameOfUser] = useState<string>('');
  const { nameOfUser: name } = useUserStore();
  const { isAuth, isLogout, setAuth, setLogout } = useModalStore();
  const { isAuthorized } = useAuthStore();

  useEffect(() => {
    if (isAuthorized) {
      const fetchUsername = async () => {
        await UserService.nameOfUser().then((res) => {
          if (res) {
            setNameOfUser(res.response.data.name);
          }
        });
      };
      fetchUsername();
    }
  }, [isAuthorized, name]);

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
          {isAuthorized && (
            <>
              <NavLink
                className={({ isActive }) => classNames(styles.headerText, { [styles.headerTextActive]: isActive })}
                to={'/favourites'}
                state={{ from: location.pathname }}
              >
                Избранное
              </NavLink>
            </>
          )}
        </nav>
      </div>

      <div className={styles.authFlex}>
        <div className={styles.userAvatarBox}>
          <img src={userAvatar} alt="User Icon" className={styles.userAvatarImage} />
        </div>
        {isAuthorized && nameOfUser ? (
          <div className={styles.authBox}>
            <Link className={styles.authLink} to={'profile'}>
              <p className={styles.authText}>Привет, {nameOfUser}</p>
              <div className={styles.line}></div>
            </Link>
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
