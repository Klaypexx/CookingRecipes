import classNames from 'classnames';
import React from 'react';
import ButtonProps from '../../../Types/ButtonProps';
import styles from './BaseButton.module.css';

const BaseButton: React.FC<ButtonProps> = ({ primary, className, type, form, buttonText, onClick, children }) => {
  const classNameBase = classNames(primary ? styles.buttonPrimary : styles.buttonSecondary, styles.baseButton);

  const textClassNameBase = classNames(primary ? styles.textPrimary : styles.textSecondary, styles.baseText);

  return (
    <button
      form={form}
      type={type ? type : 'button'}
      className={classNames(classNameBase, className)}
      onClick={onClick}
    >
      {children}
      {buttonText ? <p className={textClassNameBase}>{buttonText}</p> : null}
    </button>
  );
};

export default BaseButton;
