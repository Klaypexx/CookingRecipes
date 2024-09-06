import classNames from 'classnames';
import { ErrorMessage, Field } from 'formik';
import selectArrow from '../../../resources/icons/select-arrow.svg';
import BaseFieldProps from '../../../Types/BaseFieldProps';
import style from './BaseField.module.css';

const BaseField: React.FC<BaseFieldProps> = ({
  margin,
  select,
  type,
  as,
  className,
  name,
  labelText,
  placeholder,
  maxLength,
  styles,
  children,
}) => {
  return (
    <div className={classNames(margin && style.marginBox, style.baseBox)}>
      {select && <img src={selectArrow} alt="select arrow" className={style.selectArrow} />}
      <Field
        name={name}
        id={name}
        type={type}
        placeholder={placeholder || ' '}
        as={as}
        maxLength={maxLength}
        className={classNames(className, style.baseField, select && style.selectField, labelText && style.textPadding)}
        styles={styles}
      >
        {children}
      </Field>
      {labelText && (
        <label htmlFor={name} className={style.baseFieldLabel}>
          {labelText}
        </label>
      )}
      <ErrorMessage name={name} component="div" className={style.baseErrorStyle} />
    </div>
  );
};

export default BaseField;
