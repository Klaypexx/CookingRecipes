/* eslint-disable @typescript-eslint/no-explicit-any */
import React from 'react';
import { Formik, Form } from 'formik';
import classNames from 'classnames';
import styles from './BaseFormLabel.module.css';
import FormProps from '../../../Types/FormProps';

const BaseForm: React.FC<FormProps> = ({
  primary,
  id,
  initialValues,
  validationSchema,
  onSubmit,
  errorText,
  children,
}) => {
  return (
    <Formik initialValues={initialValues} validationSchema={validationSchema} onSubmit={onSubmit}>
      {() => (
        <Form id={id} className={classNames(primary ? styles.baseFormStyle : undefined)}>
          {errorText && <h4 className={styles.errorText}>{errorText}</h4>}
          {children}
        </Form>
      )}
    </Formik>
  );
};

export default BaseForm;
