import { useNavigate } from "react-router-dom";
import AuthService from "../../../Services/AuthService";
import useModalStore from "../../../Stores/useModalStore";
import BaseModal from "../BaseModal/BaseModal"
import styles from "./LogoutModule.module.css"
import LinkBlock from "../../Link/LinkBlock/LinkBlock";

const LogoutModal = () => {
    const navigate = useNavigate();
    const {unsetAll} = useModalStore();

    const handleExit = () => {
        unsetAll();
    }

    const handleLogout = async () => {
        await AuthService.logout();
        navigate(0);
    }

    return (
        <>
            <BaseModal 
                haederText="Уверены что хотите выйти?" 
                headerClassName={styles.headerText}
                hasAccountText="У меня еще нет аккаунта"  
            >
                <LinkBlock linkPrimaryText="Выйти" linkSecondaryText="Отмена" onClickPrimary={handleLogout} onClickSecondary={handleExit}/>
            </BaseModal>
        </>
    )
}

export default LogoutModal;