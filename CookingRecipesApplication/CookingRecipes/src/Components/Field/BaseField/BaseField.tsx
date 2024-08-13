import classNames from 'classnames';
import { ErrorMessage, Field } from 'formik';
import selectArrow from '../../../resources/icons/select-arrow.svg';
import style from './BaseField.module.css';
import BaseFieldProps from '../../../Types/BaseFieldProps';

const BaseField: React.FC<BaseFieldProps> = ({
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
    <div className={classNames(margin && style.marginBox, style.baseBoxStyle)}>
      {select && <img src={selectArrow} alt="select arrow" className={style.selectArrow} />}
      <Field
        name={name}
        type={type}
        as={as}
        placeholder={placeholder}
        maxLength={maxLength}
        className={classNames(className, style.baseFieldStyle, select && style.selectField)}
        styles={styles}
      >
        {children}
      </Field>
      <ErrorMessage name={name} component="div" className={style.baseErrorStyle} />
    </div>
  );
};

export default BaseField;
