import React, { CSSProperties } from 'react';
import styles from './BaseButton.module.css'
import classNames from 'classnames';

interface ButtonProps {
  primary?: boolean;
  className?: string;
  type?: "button" | "reset" | "submit" | undefined;
  navigation?: string;
  newStyle?: CSSProperties,
  buttonText?: string;
  onClick?: () => void;
  children?: React.ReactNode;
}

const BaseButton: React.FC<ButtonProps> = ({ primary, className, type, newStyle, buttonText, onClick, children }) => {

  const classList = classNames
    (
        primary ? styles.buttonPrimary : styles.buttonSecondary,
        styles.baseButton
    )

  const textClassList = classNames
    (
        primary ? styles.textPrimary : styles.textSecondary,
        styles.baseText
    )

  const styleList = {
    ...newStyle,
    ...(primary ? { backgroundColor: 'rgb(253, 177, 0)'} : undefined)
  }

  return (
    <button type={type} className={classNames(classList, className)} style={styleList} onClick={onClick}>
      {children}
      {buttonText ? <p className={textClassList}>{buttonText}</p> : null}
    </button>
  );
};

export default BaseButton;