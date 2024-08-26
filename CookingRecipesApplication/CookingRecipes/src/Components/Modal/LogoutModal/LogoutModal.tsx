import AuthService from '../../../Services/AuthService';
import useModalStore from '../../../Stores/useModalStore';
import BaseModal from '../BaseModal/BaseModal';
import styles from './LogoutModule.module.css';
import LinkBlock from '../../Link/LinkBlock/LinkBlock';
import { successToast } from '../../Toast/Toast';
import { useNavigate } from 'react-router-dom';
import useAuthStore from '../../../Stores/useAuthStore';

const LogoutModal = () => {
  const { unsetAll } = useModalStore();
  const navigate = useNavigate();
  const { setAuthorized } = useAuthStore();

  const handleExit = () => {
    unsetAll();
  };

  const handleLogout = async () => {
    await AuthService.logout();
    successToast('Вы успешно вышли из системы!');
    unsetAll();
    setAuthorized(false);
    navigate('/');
  };

  return (
    <>
      <BaseModal
        haederText="Уверены что хотите выйти?"
        headerClassName={styles.headerText}
        hasAccountText="У меня еще нет аккаунта"
      >
        <LinkBlock
          linkPrimaryText="Выйти"
          linkSecondaryText="Отмена"
          onClickPrimary={handleLogout}
          onClickSecondary={handleExit}
        />
      </BaseModal>
    </>
  );
};

export default LogoutModal;
