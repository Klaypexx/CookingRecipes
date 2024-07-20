import React, { CSSProperties } from 'react';
import { Link } from 'react-router-dom';
import styles from './Button.module.css'
import classNames from 'classnames';

interface ButtonProps {
  primary?: boolean;
  navigation?: string;
  newStyle?: CSSProperties,
  buttonText?: string;
  onClick?: () => void;
  children?: React.ReactNode;
}

const Button: React.FC<ButtonProps> = ({ primary, navigation, newStyle, buttonText, onClick, children }) => {

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
    <Link to={navigation ? navigation : "/"} className={classList} style={styleList} onClick={onClick}>
      {children}
      {buttonText ? <p className={textClassList}>{buttonText}</p> : null}
    </Link>
  );
};

export default Button;