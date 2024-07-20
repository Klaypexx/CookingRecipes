import React from "react";
import styles from "./ButtonBlock.module.css";
import BaseButton from "../BaseButton/BaseButton";

interface ButtonBlockProps {
    primaryType?: "button" | "reset" | "submit" | undefined;
    secondaryType?: "button" | "reset" | "submit" | undefined;
    primaryButtonText?: string;
    secondaryButtonText?: string;
    onClickPrimary?: () => void;
    onClickSecondary?: () => void;
}

const ButtonBlock: React.FC<ButtonBlockProps> = ({primaryType, secondaryType, primaryButtonText, secondaryButtonText, onClickPrimary, onClickSecondary}) => {
    return (
        <div className={styles.buttonBlock}>
            <BaseButton primary type={primaryType} buttonText={primaryButtonText} onClick={onClickPrimary}></BaseButton>
            <BaseButton type={secondaryType} buttonText={secondaryButtonText} onClick={onClickSecondary}></BaseButton>
        </div>
    )
}

export default ButtonBlock;