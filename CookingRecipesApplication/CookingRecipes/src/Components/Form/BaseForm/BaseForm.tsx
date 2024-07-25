/* eslint-disable @typescript-eslint/no-explicit-any */
import React from 'react';
import { Formik, Form } from 'formik';
import classNames from 'classnames';
import styles from './BaseFormLabel.module.css';

interface FormProps {
  primary?: boolean;
  initialValues: any; // Use a more specific type if possible
  validationSchema?: any;
  onSubmit: (values: any) => void; // Use a more specific type if possible
  errorText?: string;
  children?: React.ReactNode;
}

const BaseForm: React.FC<FormProps> = ({ primary, initialValues, validationSchema, onSubmit, errorText, children }) => {
  return (
    <Formik initialValues={initialValues} validationSchema={validationSchema} onSubmit={onSubmit}>
      {() => (
        <Form className={classNames(primary ? styles.baseFormStyle : undefined)}>
          {errorText && <h4 className={styles.errorText}>{errorText}</h4>}
          {children}
        </Form>
      )}
    </Formik>
  );
};

export default BaseForm;
