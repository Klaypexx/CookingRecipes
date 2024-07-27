import classNames from 'classnames';
import { ErrorMessage, Field } from 'formik';
import selectArrow from '../../../resources/icons/select-arrow.svg';
import style from './FormField.module.css';
import { FormFieldProps } from '../../../Types/types';

const FormInput: React.FC<FormFieldProps> = ({
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
