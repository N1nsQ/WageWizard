import { API_BASE } from "../config";
import "../App.css";
import { useTranslation } from "react-i18next";

interface Img300pxProps {
  imageUrl: string | undefined | null;
  alt?: string;
}

const Img300px = ({ imageUrl, alt }: Img300pxProps) => {
  const { t } = useTranslation();
  const defaultImg = "/default.png";
  const img300px = "?width=300&height=300";
  const src = imageUrl ? `${API_BASE}${imageUrl}${img300px}` : defaultImg;

  const altText = imageUrl
    ? alt && alt.trim() !== ""
      ? alt
      : t("employees.imageNotAvailable")
    : t("employees.imageNotAvailable");

  return <img className="employee-image" src={src} alt={altText} />;
};

export default Img300px;
