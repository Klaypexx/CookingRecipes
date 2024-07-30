import styles from './Header.module.css';
import logo from '../../resources/img/Logo.png';
import userIcon from '../../resources/icons/user.svg';
import exitIcon from '../../resources/icons/exit.svg';
import { Link, NavLink } from 'react-router-dom';
import classNames from 'classnames';
import { useEffect, useState } from 'react';
import UserService from '../../Services/UserService';
import useModalStore from '../../Stores/useModalStore';
import TokenService from '../../Services/TokenService';
import RecipeService from '../../Services/RecipeService';
const Header = () => {
  const [userName, setUserName] = useState('');
  const { isRegister, isLogin, isAuth, isLogout, setAuth, setLogout } = useModalStore();
  const token = TokenService.getAccessToken();

  useEffect(() => {
    if (token) {
      const fetchUsername = async () => {
        const response = await UserService.username();
        const { userName } = response;
        setUserName(userName);
      };
      fetchUsername();
    }
  }, [token]);

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
          <Link to={'/'} state={{ from: location.pathname }}>
            <img src={logo} alt="header_logo" className={styles.imageLogo} />
          </Link>
          <div className={styles.containerButtons}>
            <NavLink
              className={({ isActive }) => classNames(styles.headerText, { [styles.headerTextActive]: isActive })}
              to={'/'}
              state={{ from: location.pathname }}
            >
              Главная
            </NavLink>
            <NavLink
              className={({ isActive }) => classNames(styles.headerText, { [styles.headerTextActive]: isActive })}
              to={'/recipes'}
              state={{ from: location.pathname }}
              onClick={RecipeService.getAllUserRecipes}
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
          {token ? (
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
