/* eslint-disable @typescript-eslint/no-explicit-any */
import classNames from "classnames";
import closeIcon from "../../resources/icons/close.svg"
import useModalStore from "../../Stores/useModalStore";
import Button from "../Button/Button";
import styles from "./BaseModal.module.css"

interface ModalProps {
    primary?: boolean;
    headerClassName?: string | string[];
    haederText?: string;
    hasAccountText?: string;
    btnPrimaryText: string;
    btnSecondaryText: string;
    onClickPrimary?: () => void;
    onClickSecondary?: () => void;
    children?: React.ReactNode;
}

const BaseModal: React.FC<ModalProps> = ({primary, headerClassName, haederText, hasAccountText, btnPrimaryText, btnSecondaryText, onClickPrimary, onClickSecondary, children}) => {
    const {isLogin, isRegister, setLogin, setRegister, unsetAll} = useModalStore();

    const onWrapperClick = (event: any) => {
        if (event.target.classList.contains(styles.modalWrapper)) handleExit();
    };

    const handleExit = () => {
        unsetAll();
    }

    const handlerRedirection = () => {
        setLogin(isLogin)
        setRegister(isRegister);
    }
    
    return (
        <>
            <div className={styles.modal}>
                <div className={styles.modalWrapper} onClick={onWrapperClick}>
                    <div className={styles.modalContent}>
                        <button className={styles.modalCloseButton} onClick={handleExit}>
                            <img src={closeIcon} alt="closeIcon" className={styles.modalIcon}/>
                        </button>
                        <h3 className={classNames(headerClassName)}>{haederText}</h3>
                        {children}
                        <div className={styles.buttonBlock}>
                            <Button primary buttonText={btnPrimaryText} onClick={onClickPrimary}></Button>
                            <Button buttonText={btnSecondaryText} onClick={onClickSecondary}></Button>
                        </div>
                        {primary ? 
                            <div className={styles.accountBlock}>
                                <p className={styles.hasAccount} onClick={handlerRedirection}>{hasAccountText}</p>
                            </div>
                        : null}
                    </div>
                </div>
            </div>
        </>
    )
}

export default BaseModal;