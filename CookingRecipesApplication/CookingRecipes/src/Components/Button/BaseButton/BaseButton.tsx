import React from 'react';
import styles from './BaseButton.module.css';
import classNames from 'classnames';
import ButtonProps from '../../../Types/ButtonProps';

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
