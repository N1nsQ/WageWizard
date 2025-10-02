export const formatCurrency = (amount: number | undefined): string => {
  if (amount === undefined || amount === null) return "-";
  return new Intl.NumberFormat("fi-FI", {
    style: "currency",
    currency: "EUR",
    minimumFractionDigits: 2,
    maximumFractionDigits: 2,
  }).format(amount);
};
