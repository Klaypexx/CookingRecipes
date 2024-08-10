export default interface FormProps {
  primary?: boolean;
  id?: string;
  initialValues: any;
  validationSchema?: any;
  onSubmit: (values: any) => void;
  errorText?: string;
  children?: React.ReactNode;
}
