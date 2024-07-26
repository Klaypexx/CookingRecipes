import classNames from 'classnames';
import { ErrorMessage, Field } from 'formik';
import selectArrow from '../../../resources/icons/select-arrow.svg';
import style from './FormInput.module.css';

interface ButtonProps {
  margin?: boolean;
  select?: boolean;
  type?: string;
  as?: string;
  className?: string;
  name: string;
  placeholder?: string;
  maxLength?: number;
  styles?: string;
  children?: React.ReactNode;
}

const FormInput: React.FC<ButtonProps> = ({
  margin,
  select,
  type,
  as,
  className,
  name,
  placeholder,
  maxLength,
  styles,
  children,
}) => {
  return (
    <div className={classNames(margin ? style.marginBox : undefined, style.baseBoxStyle)}>
      {select ? <img src={selectArrow} alt="select arrow" className={style.selectArrow} /> : undefined}
      <Field
        name={name}
        type={type}
        as={as}
        placeholder={placeholder}
        maxLength={maxLength}
        className={classNames(className, style.baseFieldStyle, select ? style.selectField : undefined)}
        styles={styles}
      >
        {children}
      </Field>
      <ErrorMessage name={name} component="div" className={style.baseErrorStyle} />
    </div>
  );
};

export default FormInput;
