import React from 'react';
import ButtonBlockProps from '../../../Types/ButtonBlockProps';
import BaseButton from '../BaseButton/BaseButton';
import styles from './ButtonBlock.module.css';

const ButtonBlock: React.FC<ButtonBlockProps> = ({
  primaryType,
  secondaryType,
  primaryButtonText,
  secondaryButtonText,
  onClickPrimary,
  onClickSecondary,
}) => {
  return (
    <div className={styles.buttonBlock}>
      <BaseButton primary type={primaryType} buttonText={primaryButtonText} onClick={onClickPrimary}></BaseButton>
      <BaseButton type={secondaryType} buttonText={secondaryButtonText} onClick={onClickSecondary}></BaseButton>
    </div>
  );
};

export default ButtonBlock;
