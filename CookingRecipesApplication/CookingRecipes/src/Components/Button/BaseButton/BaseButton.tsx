import React from 'react';
import styles from './BaseButton.module.css';
import classNames from 'classnames';
import ButtonProps from '../../../Types/ButtonProps';

const BaseButton: React.FC<ButtonProps> = ({ primary, className, type, newStyle, buttonText, onClick, children }) => {
  const classList = classNames(primary ? undefined : styles.buttonSecondary, styles.baseButton);

  const textClassList = classNames(primary ? styles.textPrimary : styles.textSecondary, styles.baseText);

  const styleList = {
    ...newStyle,
    ...(primary ? { backgroundColor: 'rgb(253, 177, 0)' } : undefined),
  };

  return (
    <button
      type={type ? type : 'button'}
      className={classNames(classList, className)}
      style={styleList}
      onClick={onClick}
    >
      {children}
      {buttonText ? <p className={textClassList}>{buttonText}</p> : null}
    </button>
  );
};

export default BaseButton;
