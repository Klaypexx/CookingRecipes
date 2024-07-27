import BaseButton from '../BaseButton/BaseButton';
import plusImg from '../../../resources/icons/plus-orange.svg';
import styles from './AddRecipeButton.module.css';
import { AddRecipeButtonProps } from '../../../Types/types';

const AddRecipeButton: React.FC<AddRecipeButtonProps> = ({ primary, className }) => {
  return (
    <>
      <BaseButton primary={primary} buttonText="Добавить рецепт" className={className}>
        <img src={plusImg} className={styles.plus} />
      </BaseButton>
    </>
  );
};

export default AddRecipeButton;
