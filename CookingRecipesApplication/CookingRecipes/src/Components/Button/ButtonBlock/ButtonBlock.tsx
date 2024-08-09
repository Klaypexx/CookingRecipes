import React from 'react';
import styles from './ButtonBlock.module.css';
import BaseButton from '../BaseButton/BaseButton';
import ButtonBlockProps from '../../../Types/ButtonBlockProps';

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
