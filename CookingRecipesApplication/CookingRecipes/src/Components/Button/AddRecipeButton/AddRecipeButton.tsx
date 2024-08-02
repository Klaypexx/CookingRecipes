import BaseButton from '../BaseButton/BaseButton';
import plusImg from '../../../resources/icons/plus-orange.svg';
import styles from './AddRecipeButton.module.css';
import AddRecipeButtonProps from '../../../Types/AddRecipeButtonProps';

const AddRecipeButton: React.FC<AddRecipeButtonProps> = ({ primary, buttonText, className, onClick }) => {
  return (
    <>
      <BaseButton primary={primary} buttonText={buttonText} className={className} onClick={onClick}>
        <img src={plusImg} className={styles.plus} />
      </BaseButton>
    </>
  );
};

export default AddRecipeButton;
