import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";
import { AdapterDateFns } from "@mui/x-date-pickers/AdapterDateFns";
import { useTranslation } from "react-i18next";
import type { ReactNode } from "react";
import { fi, enUS } from "date-fns/locale";

interface Props {
  children: ReactNode;
}

const DatePickerLocalizationProvider = ({ children }: Props) => {
  const { i18n } = useTranslation();
  const locale = i18n.language === "fi" ? fi : enUS;

  return (
    <LocalizationProvider dateAdapter={AdapterDateFns} adapterLocale={locale}>
      {children}
    </LocalizationProvider>
  );
};

export default DatePickerLocalizationProvider;
