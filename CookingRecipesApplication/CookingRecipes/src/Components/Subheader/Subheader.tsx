import classNames from "classnames";
import styles from "./Subheader.module.css";
import arrow from "../../resources/icons/backward-arrow.svg";
import { NavLink } from "react-router-dom";
import BaseButton from "../Button/BaseButton/BaseButton";

interface SubheaderProps {
    backward?: boolean;
    headerText: string;
    btn?: boolean;
    buttonText?: string;
    onClick?: () => void;
    children?: React.ReactNode;
}

const Subheader: React.FC<SubheaderProps> = ({ backward, headerText, btn, buttonText, onClick, children }) => {
    const classList = classNames(
        backward ? styles.backward : undefined,
        styles.subheaderContainer
    );

    return (
        <section>
            <div className={classList}>
                {backward && (
                    <NavLink to={"/"} className={styles.backwardBox}>
                        <img src={arrow} alt="backward arrow" className={styles.arrow} />
                        <p className={styles.backwardText}>Назад</p>
                    </NavLink>
                )}
                <div className={styles.subheadBox}>
                    <h2>{headerText}</h2>
                    {btn && (
                        <BaseButton primary buttonText={buttonText} onClick={onClick}>
                            {children}
                        </BaseButton>
                    )}
                </div>
            </div>в
        </section>
    );
}

export default Subheader;