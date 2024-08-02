/* eslint-disable @typescript-eslint/no-explicit-any */
import classNames from 'classnames';
import closeIcon from '../../../resources/icons/close.svg';
import useModalStore from '../../../Stores/useModalStore';
import styles from './BaseModal.module.css';
import ModalProps from '../../../Types/ModalProps';

const BaseModal: React.FC<ModalProps> = ({ primary, headerClassName, haederText, hasAccountText, children }) => {
  const { isLogin, isRegister, setLogin, setRegister, unsetAll } = useModalStore();

  const handleExit = () => {
    unsetAll();
  };

  const handlerRedirection = () => {
    setLogin(isLogin);
    setRegister(isRegister);
  };

  return (
    <>
      <div className={styles.modal}>
        <div className={styles.modalWrapper}>
          <div className={styles.modalContent}>
            <button className={styles.modalCloseButton} onClick={handleExit}>
              <img src={closeIcon} alt="closeIcon" className={styles.modalIcon} />
            </button>
            <h3 className={classNames(headerClassName)}>{haederText}</h3>
            {children}
            {primary ? (
              <div className={styles.accountBlock}>
                <p className={styles.hasAccount} onClick={handlerRedirection}>
                  {hasAccountText}
                </p>
              </div>
            ) : null}
          </div>
        </div>
      </div>
    </>
  );
};

export default BaseModal;
